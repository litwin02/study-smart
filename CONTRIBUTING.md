## Requirements
- Docker

## Contributing
To contribute, first off create a `.env` file in the root directory, which should contain:
```json
DATABASE_CONNECTION_STRING="<connection_string>"
```
where `<connection_string>` is replaced with your actual connection string to the database.

## Running development mode
To run the application locally, run that command:
```
docker-compose up --build
```
This command should rebuild the image and run the API on port 80.
