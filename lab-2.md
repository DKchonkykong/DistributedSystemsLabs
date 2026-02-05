# Distributed Systems: Pipes and Filters



[← Lab 1](lab-1.md) | [← Back to Main README](README.md) | [Next: Lab 3 →](lab-3.md)



## SMART Objectives
Implementing Pipes and filters architecture
Implementing Endpoints
Implementing multiple different clients e.g., hex, binary, bytes
Transforming plaintext into bytes,hex and binary and back again through filters 
Being able to recieve Client side response back
## Completed Tasks

implemented a small server that can request and respond through the use of the pipes and filters architectural pattern wherein messages are transformed back to plaintext when it reaches the endpoint.


This is what it looks like running:
<img width="1115" height="628" alt="image" src="https://github.com/user-attachments/assets/6fda1c99-8dea-405b-a995-6cd7fd9f584d" />


## Reflection

In this lab I have implemented the pipes and filters architectural pattern which simulates a sort of request and response through a server. The messages that was sent client-side was passed through an incoming pipe where it passes through multiple different filters checking it's autheticity and translating the body of the text and converting it into a string. Then I had to get it working for the endpoints which only works in plaintext which is why I had to convert it to a string. Afterwards it goes through an outgoing pipe which also has multiple filters (reverse of incoming pipe) resulting in the client reciving a response back.

Overall, while it was at first easy it was a bit hard to understand at first since I thought data was send to one place and the other while that wasn't the case in here since this more or less middleware but instead of using an app/service to do this we just coded this by outselves.

**Navigation:**

* [Main README](README.md)
* [Lab 1](lab-1.md)
* [Lab 3](lab-3.md)
