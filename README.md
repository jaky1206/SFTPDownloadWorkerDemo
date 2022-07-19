> Please disable the antivirus while running this application as antivirus can prevent the application from creating folders.
> To create a service from executable run sc.exe \\myserver create NewService binpath= c:\windows\system32\NewServ.exe

## Demo project for SFTP Download using Worker Serive
---

**Backend service using .Net core 6. The Service includes:**
- Every 1 minute service connects to sftp and checks if there are new files.
- Sftp server, file paths etc. are configurable (not in code). 
- Service downloads all the new files to local path.
- All downloaded files (paths) are stored in database (postgresql).
- Files from sftp are never deleted, so checking if file is new or old is done by checking it in database taken in consideration file creation time.
- Working with database is done by Entity framework
- Database is defined by code first principle.
- Service is resilient: handle connection problems etc. and should not "die".
- Code have comments explaining what it does.
- Service have sane logging, configurable tracing (it should be clear what is happening from logs).
- Service uses dependency injection
