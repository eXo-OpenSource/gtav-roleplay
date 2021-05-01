import HomeIcon from "@material-ui/icons/Home";
import {useNotifications} from "./useNotifications";
import {CSSTransition} from "../../components/cssTransition";

export const NotificationAlert = () => {

    const {currentAlert} = useNotifications();

    return (
        <div className="absolute mt-2 flex justify-center align-center text-black w-full">
            <CSSTransition in={!!currentAlert} timeout={500} enter="transform ease-out duration-500 transition"
                           enterFrom="opacity-0 scale-50"
                           enterTo="opacity-100 scale-100"
                           leave="transform ease-out duration-500 transition"
                           leaveFrom="opacity-100 scale-100"
                           leaveTo="opacity-0 scale-50">
                <div className="z-50 mx-2 bg-white rounded w-full flex">
                    <div className="icon my-auto mx-2">
                        <HomeIcon/>
                    </div>
                    <div className="my-1 w-full mr-2">
                        <div className="text-xl">{currentAlert?.title}</div>
                        <div>{currentAlert?.content}</div>
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
            </CSSTransition>
        </div>
    )
}