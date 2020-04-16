# RRentalWeb

Please note that this uses Asp.net Code First migration which means the database is generated from the code. To do this the first time select RRental.Web and RRental.Web.Api as Multiple startup projects and run command in Package Manager Console:

EntityFramework\update-database -project RRental.Web.Api

Open RRental.Web.Api project properties, select the Web tab and mark "Don't open a page. Wait for a request from an external application".

Alternatively you can open RRental.Web.Api in Swagger mode for exploring API endpoints by entering the Start URL in project properties: http://localhost:61220/swagger


