# Overview

In this folder, I have shipped a C# solution `WebjetMovie` to host the API backend and a react frontend in the /frontend/movie-website.

I'm using dotnet core 3.1. I know it is out of date, but this is a borrowed laptop and the laptop owner told me that he couldn't updated the dotnet to latest version due to hardware restrictions.  My own working laptop also has restrictions to set up dotnet development environment given that my current job doesn't use C#.

# How to run
Open the C# solution and run it. The API will be run on http://localhost:5000.  There are 2 endpoints. 
`api/movies` to return all movies with some basic info.
`api/movies/detail/?cwid={...}&fwid={...}` will return movie detail including the best price for the given movie.  

for frontend, run
```
cd frontend/movie-website
npm install
npm start
```
the website shall be up and running.

## Unit tests
for C#, run test through visual studio
for frontend, `npm run test`

# Design considerations

## Two step user flow
 The website will firstly display a listing of movies, then user need to select the movie to see more details in a popup modal.  
 The reason behind this is, potentially there can be a large number of movies, try to return all details in one call will result in large volumn of API calls and long delay.

## Dealing with flacky API endpoints
As stated in the requirement, the given APIs are slow and unstale. I'm taking the following approach.

- set a timeout of 10 seconds for each given API call
- When the given API didn't return a successful response, or timeout,  I'll log a message via ILogger, and then simply return an null/empty resultset. 
- when one endpoint responds correctly while the other endpoint failed, it will directly use the result from the working endpoint. 
- when both endpoints failed, it returns empty result.
- When frontend couldn't get a successful response from backend API, it will display a simple error message.