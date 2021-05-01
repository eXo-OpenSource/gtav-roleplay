declare var alt: {
    on: (eventName: string, handler: Function) => void,
    emit: (eventName: string, data?) => void,
}