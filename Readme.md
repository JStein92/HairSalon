# Word Counter #
## By Jonathan Stein ##
## _A hair salon's internal website that allows the user to manage stylists and their clients._ ##
___




### User Story


- As a salon employee, I need to be able to see a list of all our stylists.
- As an employee, I need to be able to select a stylist, see their details, and see a list of all clients that belong to that stylist.
- As an employee, I need to add new stylists to our system when they are hired.
- As an employee, I need to be able to add new clients to a specific stylist.
- As an employee, I need to be able to update a client's name.
- As an employee, I need to be able to delete a client if they no longer visit our salon.


### Technical Specs - each spec relates to a test method

| App Behavior | Input | Actual |
|----|----|----|  
|  Get a list of all sylists from DB | List of all sylists | List of all sylists from DB |
|  Save stylist to database  |  TestStylist  |  TestStylist  |
|  Find stylist by Id | Stylist Id | Stylist Id from DB |
| Save client to database | TestClient | TestClient |
|  Find client by Id | ClientId | ClientId from DB |
|  Displays list of all clients for stylist | TestStylist | All clients in stylist category from DB |
| Update specific stylist name | StylistNewName | StylistList with updated stylist name |
| Delete specific stylist | StylistToDelete  | StylistList without StylistToDelete  |
| Update specific client name | TestClientUpdated | TestClientList with updated name |
| Delete specific client name | TestClientDeleted  | ClientList without TestClientDeleted|
| Search by Stylist name | TestStylistName | TestStylist |

### _Content_ ###

Index.cshtml:

 - Splash page - view stylists, add a new stylists, view all clients, and remove stylists

ClientDetails.cshtml
- View client details

ClientForm.cshtml
- Submit a new client

ClientsAll.cshtml
- View all clients

StylistDetails.cshtml
- View stylist details, including their clients

StylistForm.cshtml
- Submit a new stylist

Other:
- Client.cs
  - Model for client class
- HomeController.cs
  - Getting and Posting routes
- Layout.cshtml
  - CSS layout and script importing
- Styles.css
  - CSS management

### _How to use_ ###

1. Download project from GitHub: https://github.com/JStein92/HairSalon
2. Run HTML in preferred browser
3. Follow instructions on page
  - Create a new stylist and use an image from WWWRoot folder (supplied)
  - Create clients for the stylists
