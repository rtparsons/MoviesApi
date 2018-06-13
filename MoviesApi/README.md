# MoviesApi

A web API to query a movies database using c#, dotnet core 2, web API and entity framework.

Given more time i would have like to have added a suite of integration tests for the API. No unit tests included as there is very little logic outside of the data access layers.

API endpoints

A - Query movies based on filter - /api/movies
B - Show top 5 movies based on average user rating - /api/movies/top5
C - Show top 5 movies for a single user based on rating - /api/movies/user/1
D - Rate a movie - /api/movies/rate