# Event Hubs Demo

[Quickstart: Send events to and receive events from Azure Event Hubs using .NET](https://learn.microsoft.com/en-us/azure/event-hubs/event-hubs-dotnet-standard-getstarted-send)

## Hoe is dit anders dan een service bus?

Een service bus werkt met het pull en delete model. Als een consumer deze oppakt kunnen anderen er niet meer bij en is het bericht weg of dead-lettered. Met event-hubs kunnen meerdere consumers op hen eigen tempo alle events uitlezen. Als een consumer applicatie aflsuit kan deze weer opstarten en verdergaan waar deze gebleven was.


## **Key Summary: Why is Event Hubs Event-Based and Not Message-Based?**

| Feature                | Event Hubs 🟢 (Event Streaming)    | Service Bus 🔵 (Message Queue)            |
| ---------------------- | --------------------------------- | ---------------------------------------- |
| **Data Model**         | Continuous event stream           | Discrete messages                        |
| **Consumption**        | Read at own pace (offset-based)   | Pull messages (remove after processing)  |
| **Multiple Consumers** | Yes, each can have its own offset | No, unless using Topics/Subs             |
| **Retention**          | Time-based (e.g., 7 days)         | Until processed (or expired)             |
| **Ordering**           | Only within partitions            | Guaranteed (FIFO in queues)              |
| **Usage**              | Telemetry, logging, analytics     | Commands, workflows, stateful processing |

Would you say this helps clarify the difference between Event Hubs and Service Bus?
