Assumptions made: 
1. All CRUD operations are made using entity ids for simplicity.
2. When creating a beer, if it is not linked to any brewery then a default brewery is created for this beer.
3. When adding a sale of an existing beer to an existing wholesaler, the wholesaler stock for this beer is increased.
     i. If the stock does not exist for this beer then is is created
5. When a quote request is successful from the wholesaler, the wholesaler stock for this beer is decreased.
6. For the simplicity of the project and to avoid cascading on deleting and hence maintain data integrity, foreign keys are not linked to BeerId column in SaleOrders and Stocks tables.
    i. This it to say that if a beer is deleted, the related sale order and stocks records should not be deleted since they are to keep track of records.
