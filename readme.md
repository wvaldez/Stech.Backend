# Sech backend

Code challenge for Stech backend position

## Web API

I've code this web api with .net Core 5.0, I've applied the Clean architecture idea and a some aspects of the domain driven design. For the datastore I decided to use Entityframework and for testing purpose and in the sake of simplicity I decided to use the in memory framework. I've used the repository pattern for data management layer. 

For the console app, I've tried to make the 5000 async calls to run at the same time, also there's a function at the beginning of the console app to make 5 http post requests in order to create 5 books. So, for testing purpose the WebAPI can be running in the container and run the console separate.

I've enabled the OpenAPI specs by default, just to have a better experience reviewing only the WebAPI

The dockerfile is at the solution level, it doesn't need anything than docker installed, enabled for linux. Actually I've uploaded the image in azure and run a container instance.


## Live web api
[Stechwalter API](stechwalter.eastus.azurecontainer.io/swagger)

## Contact
Feel free to contact me by [email](mailto:w.valdez@outlook.com) and we can talk if you have some questions or any comments.
