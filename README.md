## Why I should use the API

This API will provide Division, District, Sub-District related data of Bangladesh. For example:

- A Division and its Districts and Sub-Districts.
- A Districts and its Division and Sub-Districts.
- A Sub-District and its Division and District.

## Where to use the API

- This types of data are frequently used in web applications where Geographical data is utilized.

## How to run the API

- The preferred way to use the API is docker. However, you need dotnet 7 to run the source code.
- If any package is missing, simply run:

```bash
dotnet restore
```

## Docker

- Docker file is added in the repo.
- Port `8080` is the exposed port for docker.
- For example, for Swagger documentation: [http://localhost:8080/swagger](http://localhost:8080/swagger)

## Running from compiled binary

- If you want to run from single executable self-contained binary, port conflict may occur. Simply add the following lines in `appsettings.json` to configure the port of kestrel server.

```json
  "Kestrel": {
    "Endpoints": {
      "HTTP": {
        "Url": "http://localhost:5169"
      }
    }
  }
```

## Environment variable

- You can create `secrets.json` in the parent directory (where `Program.cs` is located) and add any valid json value there. `Program.cs` is already configured to read from `secrets.json`
