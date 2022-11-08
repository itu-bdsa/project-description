List of Functional Requirements:

- The user should be able to specify a path to a repository
- The user should be able to switch between two modes: Commit and Frequency
- Commit-Frequency-Mode should produce a list of number of commits per day for a repository
- Commit-Author-Mode should produce a list of number of commits per days per author

List of Non-Functional Requirements:

- The project must use the libgit2sharp library to collect data from repositories.
- The code should be written in C#
- ~~The project should print statements through stdout~~
- The analyzed results should me stored in a database.
- The database should be able to be updated with new analyzed results.
- All features should be tested - unit-tests and integration-tests.
- The architecture of the system must be illustrated with a suitable diagram.
- The project should be visualized through any of the most used webbrowsers using REST API
- If the repository does not exist locally on the user's machine the repository should be stored in a temporary folder. If it exist it should be updated.
- Analysis results should be returned as results through JSON objects

NOTE: Which specific patterns will we end up using.

----------------
