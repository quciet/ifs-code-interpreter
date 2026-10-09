# Configuration
This section describes the different configuration items that need to be put in place for the different functionalities.

## RunHandler
The runhanlder is an HTTP handler module which is used for websocket interation for the *run model* functionality.

### IFsInit.db
- **RunModelProcessRetryCount**: (default: 10) Defines the number of 1000 millisecond retry perriods which are used in order to find the runmodel process with its associated process ID. This is basically the wait time, in seconds, for the run process to appear.
- **RunModelProgressFileRetryCount**: (default 10) Defines the number of 1000 millisecond retry periods which are used to wait for the progress.txt file to appear before it is considered as an error.
- **RunModelWaitForExitRetryCount**: (default 10) Defines the number of 500 millisecond retry periods which are used to wait for the model run to finished before it is considered as an error and cleaned up.