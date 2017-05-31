# EMOTIV Epoc Software Project

Software suite for EEG (Electroencephalography) data recording, viewing and storing data on server with manipulation using REST webAPI. 
Whole suite consists of **(Server application with ASP.NET WebAPI)**, **(MSSQL Database)**, **(Client software for Windows 10/WPF)** and **(OS-independent web client)** for data viewing.
Users can record subject's EEG activity levels using EMOTIV EEG headsets, categorize them into repositories and store theese data on the remote server. Data can later be accessed and manipulated with any client that communicates with server's webAPI.


# Features

### Server
Server is taking care of storing and aquiring the data from the database. Server can be accessed using specified REST WebAPI.
Using the API we can upload EEG recordings, edit or obtain them back, view users of the systems, comment on specific recordings.

### Database
Database stores all of the system's metadata. Based on Microsoft SQL Server 2017.
Database is only used for metadata, full data from recordings are saved on the disk drive as stand-alone files.

### Windows Client ("Fat Client")
An C#/WPF application that implements all of the functions provided by the server. Is also used by the recorder and can create EEG recording that can be uploaded to the server.

### Web Viewer
ASP.NET based client that implements functions provided by the server. Used primarly for data viewing, visualisation and slight data manipulation and analysis.


# Wiki

![API Documentation](test)
![The EEG recording file structure](test)
