Assumptions made: 
1. All CRUD operations are made using entity ids for simplicity.
2. When creating a beer, if it is not linked to any brewery then a default brewery is created for this beer.
3. When adding a sale of an existing beer to an existing wholesaler, the wholesaler stock for this beer is increased.
     i. If the stock does not exist for this beer then is is created
5. When a quote request is successful from the wholesaler, the wholesaler stock for this beer is decreased.
