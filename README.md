## OVERVIEW ##

DJ Valeting is a car valeting service which tours the business parks of Wakefield valeting cars on the go. DJ Valeting has approached us to solve a simple problem in an easy-to-use way, the problem being, bookings. Currently, all bookings are done over the phone and are written down manually and tracked this way, we are going to make this automated.

### GOALS ###

1. Automate the booking system 
    1. Have a client facing form to take bookings.
    2. Have a way to view the bookings.
    3. Have a way to approve the bookings.
2. Have a simple admin area which displays a table of bookings in date order.
    1. Bookings should come from a form that is client facing.
    2. DJ Valeting should be able to create, edit, delete & approve bookings manually via the admin area too.
    3. Admin area should be behind a login.
  
### SPECIFICATIONS ###

#### Technology Requirements ####
This system should be built on the latest version of .NET Core, using the Entity Framework for interacting with a database.
We would expect that there would be migrations and seeders setup so we can easily create a copy of the database on our end without having to create SQL dumps.

#### Client Facing Form ####
This should be a simple form which posts and saves to the database for DJ Valeting to then view in an admin area, the form should consist of the following fields:
1. Name (required)
2. Booking Date (required)
3. Flexibility (options below) (required)
    1. +/- 1 Day
    2. +/- 2 Days
    3. +/- 3 Days
4. Vehicle Size (options below) (required)
    1. Small
    2. Medium
    3. Large
    4. Van
5. Contact Number (required)
6. Email Address (required)

#### Admin Area ####
The admin area should be locked down behind a login system, the login form should have the following fields:
1. Email Address
2. Password (This will need hashing, you can decide a suitable secure hashing algorithm for this)

Once entering the admin area, DJ Valeting should be presented with a basic table, listing all of their bookings, the columns on the table should match the fields on the client facing form above with the addition of one more column called “Actions”, which will have a tick icon/approve button, edit icon/edit button and delete icon/delete button. Above the table, there should also be a green “Create” button, to add a new booking manually. There should also be a nav bar, which gives a way for the DJ Valeting to log out of the system.

The create button should take you to a create booking screen which contains the same fields as the initial form, plus a green submit button, any new bookings should save on submission and be reflected in the table.

The tick icon/approve button should simply mark the record as approved and the row in the table should receive a green tint in colour.

The edit icon/edit button should take you to an edit screen which contains the same fields as the initial form, plus a green submit button, with any data pre-populated. Any edits should save on submission and be reflected in the table.
The delete icon/delete button should simply remove the record and this should be reflected on the table.

### Source Control ###
In order to get an idea as to how you work with GIT, and to also timestamp how long it took you to do each section we require that you host a repository on a public facing GIT management (IE GitHub, GitLab or BitBucket). If you take breaks during this test - please create a commit labelling both the start & end of your session so we know when you started and finished working on this project.

### Bonus Material ###
- Upon approving a booking, an email should be sent to the client accepting their booking request, this should be limited so that the email cannot be spammed, it should only go out on the first ever click.
- On the client-facing form, it would be nice to have the Flexibility & Vehicle Size be dynamic and pulled from some place in the Admin Panel so DJ Valeting can customize and change these as and when necessary.
- Create Unit/Implementation tests where possible.
