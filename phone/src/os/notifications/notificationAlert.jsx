import HomeIcon from "@material-ui/icons/Home";
import {useNotifications} from "./useNotifications";
import {CSSTransition} from "react-transition-group";

export const NotificationAlert = () => {

    const {currentAlert} = useNotifications();

    return (
        <CSSTransition in={!!currentAlert} mountOnEnter unmountOnExit>
            <div className="absolute mt-2 flex justify-center align-center text-black w-full">
                <div className="z-50 mx-2 bg-white rounded w-full flex">
                    <div className="icon my-auto mx-2">
                        <HomeIcon/>
                    </div>
                    <div className="my-1 w-full mr-2">
                        <div className="text-xl">Job-Einladung</div>
                        <div>final lädt dich zum Müllarbeiter-Job ein</div>
                        <div className="flex mt-1">
                            <button type="button" className="flex-grow px-2 shadow-lg font-medium">
                                Annehmen
                            </button>
                            <button type="button" className="flex-grow px-2 shadow-lg text-red-600 font-medium">
                                Ablehnen
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </CSSTransition>
    )
}