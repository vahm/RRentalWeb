# RRentalWeb

Please note that this uses Asp.net Code First migration which means the database is generated from the code. To do this the first time select RRental.Web and RRental.Web.Api as Multiple startup projects and run command in Package Manager Console:

EntityFramework\update-database -project RRental.Web.Api

For Swagger UI set the RRental.Web.Api project properties to Start Url "swagger".


