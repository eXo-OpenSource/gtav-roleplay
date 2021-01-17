import alt from 'alt-client';
import { FaceFeaturesUi } from '../ui/FaceFeaturesUi';
import { RegisterLogin } from '../ui/RegisterLogin';

alt.onServer('Ui:ShowFaceFeatures', () => new FaceFeaturesUi());

alt.onServer('Ui:ShowRegisterLogin', () => RegisterLogin.openLogin());