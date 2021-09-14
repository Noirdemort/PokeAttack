# Target Package
 
 1. Search for a pokemon and display information about it. From a url
 2. For each Pokemon, list its name, number, an image of it, and some abilities. Add option to view all data about it.
 3. A user of the app can search by name or any other attributes like the Pokemon's abilities.
 4. There should also be a way to sort the Pokemon results by name or number (a-z, 1-N).

5. Pokemon Data will never change. Once you fetch data, it must persist on the device.

 6. Please define a storage, caching, and network API for storing and fetching data without using a 3rd party API. While the content is being fetched, you should display to the user a nice loading indicator. If there is an error retrieving a piece of data, have a placeholder.

7. Every time the app launches, you must load a reasonable amount of data from local storage and you must fetch 10 unique Pokemon from the endpoint (all Pokemon name's are unique and can be used as a unique ID). Please utilize paging for an optimal experience. You can limit fetching up to 300 Pokemon. Once you have fetched 300, no need to fetch anymore.

8. Add unit test cases.
 9. Please design the UI/UX to the best of your ability. Get creative; bonus points will be awarded to projects with good UI/UX.

https://pokeapi.co/api/v2/pokemon/?offset=0&limit=20

- Networking
- Concurrency
- Callbacks
- Codable protocols  
- URL requests
- Good architecture and code design
- Separation of responsibilities
- Proper optional and error handling 
- Caching / Storage
- Proper usage of memory and disk 
- Data persistance 
- UI
- Creating tableview from scratch
- Proper registration of cell class
- Reusable cells
- Creating custom classes 
- Protocol implementation 
- Data source
- Delegation
- Optimization 
- Pull to refresh a view with data
- Infinite scroll 
- Search results
- Build a view more details page


# Experiment

Just experimenting in F#

Well, it isn't exactly suitable as CLI or desktop (as I mainly focus on Apple Platforms). Might be better for servers.
