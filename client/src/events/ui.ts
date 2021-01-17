import alt from 'alt-client';
import Chat from '../ui/Chat';
import { FaceFeaturesUi } from '../ui/FaceFeaturesUi';
import { RegisterLogin } from '../ui/RegisterLogin';
import UiManager from '../ui/UiManager';

alt.onServer('Ui:ShowFaceFeatures', () => new FaceFeaturesUi());

alt.onServer('Ui:ShowRegisterLogin', () => RegisterLogin.openLogin());

UiManager.on("Chat:Message", Chat.handleChatMessage);
UiManager.on("Chat:Loaded", Chat.loadChat)