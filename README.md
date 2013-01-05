NTNU ITC License
================

This is a simple KMS licensing UI for softwares from NTNU ITC.

So far, I just implemented Office 2010. 

It automatically executes these script:

* For Windows XP

        cd\
        cd Program Files\Microsoft Office\Office14
        cscript ospp.vbs /osppsvcrestart
        cscript ospp.vbs /sethst:w7-kms.itc.ntnu.edu.tw
        cscript ospp.vbs /act

* For Windows 7 (or Windows 8)

        cd\
        cd Program Files\Microsoft Office\Office14    <---  Somebody may installed to "Program Files (x86)"
        cscript ospp.vbs /sethst:w7-kms.itc.ntnu.edu.tw
        cscript ospp.vbs /act
        
## Notes

### Its target framework is .NET framework 3.5.  

I checked that Windows 7 had supported it, but Windows XP didn't.
If you can't execute this program on your Windows XP,
I recommand you follow the official licensing document provided by NTNU ITC.
Because it may takes you a long time installing .NET framework. ^_<
  
### This tool is useful for current NTNU members only.

If you are not a member of [NTNU] (http://www.ntnu.edu.tw), or you have graduated from NTNU. You are not authorized to download or get DVD of Office 2010 in our school.
You can check out [NTNU ITC's desciption] (http://www.itc.ntnu.edu.tw/sw/).

## Backstory

I currently part-timed in NTNU ITC.
Some weeks ago, I found some student maybe not familiar with command line,
so they can't license their software successfully.
They tried so many times, still failed, got angry and had a bitter quarrel with our manager.
As a computer science student, I begin thinking how to make this licensing process more friendly.
So, here is the project...

## Author

adzen... A [NTNU CSIE] (http://www.csie.ntnu.edu.tw/) student :)     

    
    
        
