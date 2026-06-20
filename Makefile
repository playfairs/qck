build:
	dotnet build src/qck/qck.csproj

run:
	dotnet run --project src/qck/qck.csproj

test:
	dotnet test

clean:
	dotnet clean src/qck/qck.csproj