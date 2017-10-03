# Brainy Studio

Software suite for EEG (Electroencephalography) data recording, viewing and storing data on server with manipulation using REST webAPI. 
Whole suite consists of **(Server application with ASP.NET WebAPI)**, **(MSSQL Database)**, **(Client software for Windows 10/WPF)** and **(OS-independent web client)** for data viewing.
Users can record subject's EEG activity levels using EMOTIV EEG headsets, categorize them into repositories and store theese data on the remote server. Data can later be accessed and manipulated with any client that communicates with server's webAPI.


# Features

### Server
Server is taking care of storing and aquiring the data from the database. Server can be accessed using specified REST WebAPI.
Using the API we can upload EEG recordings, edit or obtain them back, view users of the systems, comment on specific recordings.
API is secured by web authentication and requirement of pre-registered API key.

![Api](https://s10.postimg.org/80x1ac8m1/api.png)


### Database
Database stores all of the system's metadata. It is accessed only by server application.
Database is used for metadata, full data from recordings are saved on the disk drive as stand-alone files, database only includes pointer (or filepaths) to them.

![Database Structure](https://s1.postimg.org/y2k0ie43z/epoc.png)


### Windows Client ("Fat Client")
An C#/WPF application that implements all of the API functions provided by the server. Used as the recorder and can create EEG recording that can be uploaded to the server. Can also be used as live monitor, user repositories manager, viewer, editor of subjects and user data.

![Client Screenshot 1.](https://s7.postimg.org/bw9foszmj/noe.png)


### Web Viewer
ASP.NET based client that implements functions provided by the server. Used primarly for data viewing, visualisation and slight data manipulation and analysis.


# Important notes

### File Structure
Across the board the system works with standard JSON file structure. All of the recordings are stored as JSON structured file and all of the responses and requests for API should be formatted as such.
