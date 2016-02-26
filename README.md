# _Hair Salon_

#### _An App for Managing Hair Stylists and their Clients, 2/26/2016_

#### By _**Michael Dada**_

## Description

_This application allows a hair salon owner to manage a list of their current stylists, as well as all of their respective clients.  They can add, edit and delete stylists and clients on the fly._

## Setup/Installation Requirements
* _To create your own database:
    In SQLCMD:
    CREATE DATABASE hair_salon;
    GO
    USE hair_salon;
    GO
    CREATE TABLE stylists (id INT IDENTITY(1,1), description VARCHAR(255));
    CREATE TABLE clients (id INT IDENTITY(1,1), description VARCHAR(255), stylist_id INT);
    GO_

* _In Powershell, run dnu restore_
* _To start server, run dnx kestrel_
* _Navigate to localhost:5004_
* _Enjoy!_


## Known Bugs

_There are no known bugs at this time_

## Support and contact details

_You can contact me with questions or comments at mail.michaeldada@gmail.com_

## Technologies Used

_C#, MySQL, SSMS, Razor, Nancy, Powershell, Atom_

### License

*MIT License*

Copyright (c) 2016 **_Michael Dada_**
