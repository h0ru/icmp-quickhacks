# (⚡) ICMP Qu1ck-H4cks
* [`Introduction`](#introduction)
* [`Security`](#security)
* [`Tools`](#tools)
  
---

## Introduction

- ICMP is defined in [**RFC 792**](https://www.rfc-editor.org/rfc/rfc792.html).
- ICMP (**Internet Control Message Protocol**) is the utility protocol of TCP/IP responsible for providing information regarding the availability of devices, services, or routes in a TCP/IP network.

### ICMP Type Number
<div align="center" style="display:flex;">
  <img src="https://github.com/h0ru/icmp-quickhacks/assets/117091833/5bfd03e7-0384-467e-b716-97a2e27f4cdd" width="800">
</div>

- More Type Numbers [**HERE**](https://www.iana.org/assignments/icmp-parameters)

### ICMP Structure
<div align="center" style="display:flex;">
  <img src="https://github.com/h0ru/icmp-quickhacks/assets/117091833/cbcc9fb8-f95f-470e-bb57-f5d8e1c03fbd" width="800">
</div>

- **Type**: It's an 8-bit field. It defines the ICMP message type. The values range from 0 to 127 are defined for ICMPv6, and the values from 128 to 255 are the informational messages.
- **Code**: It's an 8-bit field that defines the subtype of the ICMP message
- **Checksum**: It's a 16-bit field to detect whether the error exists in the message or not.

---

## Security

- ICMP Attacks against TCP ~ [**RFC 5927**](https://www.rfc-editor.org/rfc/rfc5927)
- Administrators face the challenge of balancing ICMP usage for network operations against potential risks. Key security concerns include:

### 1. Footprinting
- ICMP is exploited for footprinting, gathering information about targets.
- [⚔️] Ping sweeps identify live hosts, and router discovery is achieved using Solicitation messages.
-[🛡️] Blocking ICMP entirely hinders footprinting but complicates troubleshooting. A compromise is to block ICMP from untrusted networks.

### 2. Denial of Service (DoS)
- Unreachable messages can be spoofed to trick hosts into closing connections.
- [⚔️] Smurf Attacks flood a target with Echo packets, overwhelming it.
- [⚔️] The Ping of Death, an older threat, involves creating large fragmented packets to crash systems.
- [🛡️] Firewalls with ICMP rate-limiting features help mitigate these attacks.

### 3. Tunneling
- ICMP echo and echo-reply packets can be used for covert channeling.
- [⚔️] Tunneling encapsulates restricted data within seemingly ordinary traffic.
- [🛡️] Mitigation involves using application firewalls or Intrusion Prevention Systems (IPS) with deep packet inspection.

### 4. Man-In-The-Middle (MITM) Attacks
- [⚔️] ICMP redirects can be spoofed, leading hosts to use compromised routers as gateways.
- [⚔️] MITM attacks involve intercepting and potentially altering legitimate traffic.

- Balancing the benefits of ICMP with robust security measures is crucial for administrators to ensure network integrity and resilience against potential threats.

