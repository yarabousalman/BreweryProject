using BreweryProject.Controllers;
using BreweryProject.Data;
using BreweryProject.DataManagers.Data;
using BreweryProject.DataManagers.Repositories;
using BreweryProject.Entities;
using BreweryProject.Interfaces;
using BreweryProject.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TestProject1
{
    public class ValidityTests
    {
        [Fact]
        public async void TestNullOrderQuoteRequest()
        {
            var dbContext = await GetDatabaseContext();
            var repository = new Mock<WholesalerRepository>(dbContext);
            var dataResult = await repository.Object.RequestQuote(new QuoteRequest());
            Assert.Null(dataResult.Data);
            Assert.Contains("empty", dataResult.ErrorMessage);
        }


        [Fact]
        public async void TestEmptyOrderQuoteRequest()
        {
            var dbContext = await GetDatabaseContext();
            var repository = new Mock<WholesalerRepository>(dbContext);
            var dataResult = await repository.Object.RequestQuote(new QuoteRequest
            {
                BeerRequests = new List<BeerRequest>()
            });
            Assert.Null(dataResult.Data);
            Assert.Contains("empty", dataResult.ErrorMessage);
        }

        [Fact]
        public async void TestZeroAmountQuoteRequest()
        {
            var dbContext = await GetDatabaseContext();
            var repository = new Mock<WholesalerRepository>(dbContext);
            var dataResult = await repository.Object.RequestQuote(new QuoteRequest
            {
                BeerRequests = new List<BeerRequest>
                {
                    new BeerRequest
                    {
                        Id = 1,
                        Amount = 0
                    }
                },
                WholesalerId = 11
            });
            Assert.Null(dataResult.Data);
            Assert.Contains("empty", dataResult.ErrorMessage);
        }

        [Fact]
        public async void TestNonExistentWholesalerQuoteRequest()
        {
            var dbContext = await GetDatabaseContext();
            var repository = new Mock<WholesalerRepository>(dbContext);
            var dataResult = await repository.Object.RequestQuote(new QuoteRequest
            {
                BeerRequests = new List<BeerRequest>
                {
                    new BeerRequest
                    {
                        Id = 1,
                        Amount = 2
                    }
                },
                WholesalerId = 11
            });
            Assert.Null(dataResult.Data);
            Assert.Contains("exist", dataResult.ErrorMessage);
        }

        [Fact]
        public async void TestDuplicateOrderQuoteRequest()
        {
            var dbContext = await GetDatabaseContext();
            var repository = new Mock<WholesalerRepository>(dbContext);
            var dataResult = await repository.Object.RequestQuote(new QuoteRequest
            {
                BeerRequests = new List<BeerRequest>
                {
                    new BeerRequest
                    {
                        Id = 1,
                        Amount = 2
                    },
                    new BeerRequest
                    {
                        Id = 1,
                        Amount = 4
                    }
                },
                WholesalerId = 1
            });
            Assert.Null(dataResult.Data);
            Assert.Contains("duplicate", dataResult.ErrorMessage);
        }

        [Fact]
        public async void TestNonExistentBeerQuoteRequest()
        {
            var dbContext = await GetDatabaseContext();
            var repository = new Mock<WholesalerRepository>(dbContext);
            var dataResult = await repository.Object.RequestQuote(new QuoteRequest
            {
                BeerRequests = new List<BeerRequest>
                {
                    new BeerRequest
                    {
                        Id = 25,
                        Amount = 2
                    }
                },
                WholesalerId = 1
            });
            Assert.Null(dataResult.Data);
            Assert.Contains("exist", dataResult.ErrorMessage);
        }

        [Fact]
        public async void TestInsufficientBeerStockQuoteRequest()
        {
            var dbContext = await GetDatabaseContext();
            var repository = new Mock<WholesalerRepository>(dbContext);
            var dataResult = await repository.Object.RequestQuote(new QuoteRequest
            {
                BeerRequests = new List<BeerRequest>
                {
                    new BeerRequest
                    {
                        Id = 1,
                        Amount = 25
                    }
                },
                WholesalerId = 1
            });
            Assert.Null(dataResult.Data);
            Assert.Contains("not enough", dataResult.ErrorMessage);
        }

        [Fact]
        public async void TestNoDiscountQuoteRequest()
        {
            var dbContext = await GetDatabaseContext();
            var repository = new Mock<WholesalerRepository>(dbContext);
            var dataResult = await repository.Object.RequestQuote(new QuoteRequest
            {
                BeerRequests = new List<BeerRequest>
                {
                    new BeerRequest
                    {
                        Id = 1,
                        Amount = 4
                    },
                      new BeerRequest
                    {
                        Id = 2,
                        Amount = 4
                    }
                },
                WholesalerId = 1
            });
            Assert.NotNull(dataResult.Data);
            Console.WriteLine(dataResult.Data.Summary, OutputLevel.Information);
        }

        [Fact]
        public async void Test10PercentDiscountQuoteRequest()
        {
            var dbContext = await GetDatabaseContext();
            var repository = new Mock<WholesalerRepository>(dbContext);
            var dataResult = await repository.Object.RequestQuote(new QuoteRequest
            {
                BeerRequests = new List<BeerRequest>
                {
                    new BeerRequest
                    {
                        Id = 1,
                        Amount = 6
                    },
                      new BeerRequest
                    {
                        Id = 2,
                        Amount = 5
                    },
                },
                WholesalerId = 1
            });
            Assert.NotNull(dataResult.Data);
            Console.WriteLine(dataResult.Data.Summary, OutputLevel.Information);
        }

        [Fact]
        public async void Test20PercentDiscountQuoteRequest()
        {
            var dbContext = await GetDatabaseContext();
            var repository = new Mock<WholesalerRepository>(dbContext);
            var dataResult = await repository.Object.RequestQuote(new QuoteRequest
            {
                BeerRequests = new List<BeerRequest>
                {
                    new BeerRequest
                    {
                        Id = 1,
                        Amount = 10
                    },
                      new BeerRequest
                    {
                        Id = 2,
                        Amount = 11
                    },
                },
                WholesalerId = 1
            });
            Assert.NotNull(dataResult.Data);
            Console.WriteLine(dataResult.Data.Summary, OutputLevel.Information);
        }

        private async Task<DbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Breweries.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.Breweries.Add(new Brewery()
                    {
                        Id = i,
                        Name = $"Brewery {i}"
                    });
                    await databaseContext.SaveChangesAsync();
                }
            }

            if (await databaseContext.Beers.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.Beers.Add(new Beer()
                    {
                        Id = i,
                        Name = $"Beer {i}",
                        Price = 2+i
                    });
                    await databaseContext.SaveChangesAsync();
                }
            }
            if (await databaseContext.Wholesalers.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.Wholesalers.Add(new Wholesaler()
                    {
                        Id = i,
                        Name = $"Wholesaler {i}"
                    });
                    await databaseContext.SaveChangesAsync();
                }
            }

            if (await databaseContext.Stocks.CountAsync() <= 0)
            {
                for (int i = 1; i <= 4; i++)
                {
                    databaseContext.Stocks.Add(new Stock()
                    {
                        Id = i,
                        WholesalerId = 1,
                        BeerId = i,
                        Amount = 10 + i
                    });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }
    }
}