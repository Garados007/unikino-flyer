# unikino-flyer
_The tool to upload the flyer on the website for lazy dudes (like me)._

For our local [Unikino](https://www.unikino.uni-halle.de/) I have to regulary upload the weekly flyer. They are the same steps every time:

1. Login
2. Upload Image
3. Rename Image
4. Go to the home page
5. replace the image (the image is always on the last site of the image selector)
6. set the text
7. publish

And this for every week. Always the same boring steps. Because I am to lazy for that stuff I need a different solution. And this repository will be the solution: A background programm triggered by a cronjob will always check if the newest flyer are online.

Perfect!

## Setup

I don't know the reason you want to setup this repository on your computer, but here we go:

1. Make sure you have .NET Framework (job generator, only Windows) and .NET Core (both) installed
2. Make sure you have a permanent connection to the intranet of the university. In my case I have configured a split VPN connection to MLU Halle.
3. Add the updater to the cronjob of the desired computer. I used a Rasperry Pi and set it to 0:10 each morning.
4. You need to write the config.ini with correct values. Use the config.template.ini as template for it.
5. After that you need to provide a job file (generated with the job generator) to the updater.

That's it!

After that you only need to create you jobs with the job generator and upload the result file to the updater. The updater will consume this file and process it.

## How it works?

The generator collect all information, dates and images and store them in a Sqlite database. This datebase will be delivered to the updater by the user.

The updater will regulary fired by the cronjob and checks if a job to due is found. This information will be uploaded and after that the job will removed from the database. If all jobs are done you have an empty job database.
If the updater is started and no jobs are to due, it will be closed immeadetly.

If you want to know how a single job is processed just look at the code - it will tell you. ;)
