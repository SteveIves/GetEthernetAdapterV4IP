# GetEthernetAdapterV4IP

This is a simple command line utility that accepts the name of an Ethernet adapter and obtains and displays the first IPV4 address assigned to the adapter.

First use the IPCONFIG command to lookup the name of the IP address, for example:

```
Ethernet adapter vEthernet (WSL):

   Connection-specific DNS Suffix  . :
   Link-local IPv6 Address . . . . . : fe80::f139:8cfd:666f:3e08%81
   IPv4 Address. . . . . . . . . . . : 172.30.64.1
   Subnet Mask . . . . . . . . . . . : 255.255.240.0
   Default Gateway . . . . . . . . . :

```
Windows prefixes all Ethernet adapterrs with the text "Ethernet Adapter " so in the above example, the name of the adapter is "vEthernet (WSL)". Once you have the name, you can pass it to this utility as parameter 1, like this:
```
C:\> GetEthernetAdapterV4IP "vEthernet (WSL)"
```
If the adapter is found, active and has one or more V4 IP addresses, the first will be displayed to standard out:

```
172.30.64.1
```
The utility sets an exit status of 0 if an IP address is found, or 1 if not.

## Running from Linux under WSL2

As long as you put this utility somewhere in PATH on your Windows system, you can execute 
it from a WSL2 Linux bash environment.

## Example 1: Record Windows WSL2 Host IP Address in a Linux Environment Variable
My personal use case for this utility is for use in WSL2 when running Linux distributions. 
Sometimes I need communicate with some network service on the Windows host, but the IP
address of the host Windows system on the `vEthernet (WSL)` network has a tendency to 
change with every reboot. Here is an example of running the utility from bash on an 
Ubuntu 20.04 system and capturing the IP address to a Linux environment variable.

```
$ export WINHOST_IP=`GetEthernetAdapterV4IP.exe "vEthernet (WSL)"`
$ echo $WINHOST_IP
172.30.64.1
```
## Example 2: Update Synergy License Server Forwarding Address

Similar to Example 1, if you are running Synergy/DE in an WSL2 Linux environment and using
Synergy License Server Forwarding, you will need to update your license server forwarding 
IP address periodically. Here is how to do so:

```
lmu -nf `GetEthernetAdapterV4IP.exe "vEthernet (WSL)"`
```
