# GigNow
Gig:Now is a Web Application designed to facilitate streamlined booking communications and experiences between music venues and musicians. 
Gig:Now also allows listeners to search for gigs in their area and stay in the loop with customized notifications.

Venues managers can create profiles that include some very pertinent and basic info about their venues. They can create gigs and decide
what sort of artists they'd like to book for the gig. Automatic notifications and texts are then sent to all available Gig:Now Artists
who fit the Genre and type that is specified by the Venue in the gig. Artists may then apply to play, in which case, an automated notification
and text will be sent to the venue.

Artists can also create Profiles with basic info about themselves to be viewed by Venues and Listeners. When they apply to a gig, Venues
can view the profile and decide whether or not to book them. 

When a Venue accepts the application of an Artist, the artist recieves a notificaiton, the Gig's bill is updated, and the Venue's giglist
is updated.

Listeners create profiles and can watch Artists, Venues, and Gigs. When a watched Venue creates a gig, a watched Artist books a Gig, or 
a watched Gig is updated, the Listener recieves a notification and text. the Listener can also search for these within the site.

This Application was made as my capstone project at devCodeCamp and was written in C# using the ASP.NET MVC framework. It utilizes code-first
migration to update a SQL database, and JavaScript with JQuery for interactivity. GoogleMaps API is used for location and route displays
and Twilio API is used for text message functionality.
    
