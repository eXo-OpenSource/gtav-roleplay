const eventHandlers = {}


if (process.env.NODE_ENV === 'development') {
    if (!("alt" in window)) {
        console.log("did")
        window.alt = {
            on: (eventName, handler) => {
                console.log(eventName, handler)
                if (!(eventName in eventHandlers)) {
                    eventHandlers[eventName] = []
                }
                eventHandlers[eventName].push(handler)
            },
            emit: (eventName, ...data) => {
                console.log("[DEBUG] Alt-V: ", eventName, data)
            }
        }
    }
}

export interface AltEvent {
    name: string,
    data: any
}

const InjectDebugData = (events: AltEvent[], timer = 1000) => {
    if (process.env.NODE_ENV === 'development') {
        for (const event of events) {
            setTimeout(() => {
                if (event.name in eventHandlers) {
                    eventHandlers[event.name].forEach((handler) => handler(event.data))
                } else {
                    console.log("no eventHandler found for " + event.name)
                }
            }, timer);
        }
    }
};

export default InjectDebugData;