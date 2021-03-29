API written using ASP.NET Core as part of a coding test. 

Provided with a CSV filled with data about munros in the British Isles, the API was required to return them to any potential clients filtered by optional criteria. 
The criteria could be none, one or many of the following:
* A minimum height in metres
* A maximum height in metres
    * The API should return a suitable error if the maximum is less than the minimum
* A fixed limit on the number of results to return
* A category of Munro, Top, or Either
* For the results to be sorted by their name or height, ascending or descending
    * Optionally, that the results be sorted by both name and/or height, ascending or descending, in an order of preference


## Notes
I mainly focussed on writing unit tests for the service as that is where the bulk of the logic is, as the controller is merely a passthrough for the service. Similarly, most of the validation for the query parameters passed in are handled by the route filtering during model binding.

During development I tested using Postman to send requests. The full list of valid query parameters are:

* minHeightInMetres (can accept a double) - optional (defaults to 0)
* maxHeightInMetres (can accept a double) - optional (defaults to max double)
* limit (can accept an int) - optional
* category (valid values are munro, top, either (case-insensitive)) - optional (defaults to either)
* sortBy (expects a string in the format `[a|de]sc([name|height]`) e.g. desc(height) ) - optional

The sorting criteria is passed as a query parameter following the format of `[a|de]sc([name|height]`) (which is validated using a regular expression during model binding) with the possibility to comma-separate them to sort by both name and height. For example, passing `sortBy=asc(name),desc(height)` as a query parameter would mean the data would be sorted first by name ascending then by height descending. I did also add in the option to explicitly set a priority by appending a "p" followed by a single digit after the closing parenthesis (e.g. `asc(height)p2`) with the lower number being the property it sorted on first. This is still implemented but I lean more towards the implied order of preference given by the comma-separated list. I thought to add this as I was intending to allow for there to be multiple sortBy= in the URL, and since their order can't be guaranteed, some indicator of priority would be required. However, the validation didn't play nicely with the regular expression and a list of strings, which is another reason why I prefer the approach I've gone for.

An example of a valid request with all criteria specified would be:

`/api/munros?minHeightInMetres=450&maxHeightInMetres=1400&category=munro&limit=50&sortBy=asc(height),asc(name)`