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
My personal use case for this utility is for use in WSL2 when running Linux distributions. Sometimes I need to know the IP address of the host Windows system on the vEthernet (WSL) network, so that I can communicate with a netwoirk service on the Windows host. As long as the utiolity is in PATH on the Windows system, I can execute it from the WSL2 Linux system and capture the output. For example:
```
$ export WINHOST_IP=`GetEthernetAdapterV4IP.exe "vEthernet (WSL)"`
$ echo $WINHOST_IP
172.30.64.1
```
The utility sets an exit status of 0 if an IP address is found, or 1 if not.
