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

## Hardware Steps

I run the updater on a Raspberry Pi with Raspberian (Linux). Some steps has to be done so the updater can be executed without any problems. This steps are only required in my setup. Your setup could be different and therefore you need different steps.

### 1. Ensure you have a permanent VPN Tunnel to the University

I am living outside of the university network that is required to edit the stuff online. But our university supports a vpn tunnel with the CISCO vpn connector. Therefore I installed `openconnect` and `vpn-slice` on my pi. After that i ensured that a vpn tunnel
exists at the time when the updater will be fired. I used a cronjob for this. The complete call look like this:

```bash
echo "<password>" || openconnect vpn1.uni-halle.de -m 1290 -u <username> -s 'vpn-slice 141.48.0.0/16 192.124.243.0/24'
```

This will open the vpn tunnel and login with your credentials. With the `-s <...>` parameter the vpn tunnel will be splitted and only traffic to the university network will be sent over the vpn.

### 2. For testing: make your vpn avaible for other devices in the network

I used to develop and test my stuff on my working computer not the pi. But I want to use the vpn of the pi in the same network to test these. So I added the following to the pi:

```bash
iptables -A FORWARD -i eth0 -o tun1 -j ACCEPT
iptables -A FORWARD -i tun1 -o eth0 -m state --state ESTABLISHED,RELATED -j ACCEPT
iptables -t nat -A POSTROUTING -o tun1 -j MASQUERADE
```

Than I saved the iptables on the pi and added the ip range to the route list of my main computer. Afterwards everything works fine.
