# NPGSQLConnectionSetTimeout
Code and Solution to reproduce a hanging issue with the PostgresEmbed library

# Reproduction Steps
1. Clone and Build the Solution
2. Locate the test TestConnectionSetTimeoutEmbeddedCommit
3. Ensure all postgres.exe instances are cleared from previous runs (if applicable)
4. Run the test in Debug

## Actual:
The tests does not complete
In the Output window, the code never gets past 44 iterations and the trace shows
"Test 45 - Started
set statement_timeout = 120000; commit;  - Started"

## Expected:
The test completes in approx 10secs

## Notes:
- The test seems to fail due to the fact an explicit "commit" is being executed when NOT IN an Transaction.
- The these tests do not fail on a "real" Postgres installation
- When the Embedded DB Logs are turned on the following warning is in the logs "2019-11-13 14:05:44.348 PST [3416] WARNING:  there is no transaction in progress"
- The tests that open an explicit transaction never fail
- When the postgres.exe is left running from a previously failed run the test TestConnectionSetTimeoutEmbeddedCommit passes
- When the test fails and is re-run (without killing the postres.exe instances) the tests pass


