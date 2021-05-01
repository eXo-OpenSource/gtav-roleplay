import InjectDebugData from "../../os/debug/injectDebugData";

export const MessagesApp = () => {
    return (
        <div></div>
    )
}

InjectDebugData([
    {
        name: "Phone:Messages:Incoming",
        data: {
            embed: true,
            title: "Job-Einladung",
            content: "final lädt dich zum Müllarbeiter-Job ein"
        }
    }
], 2000)