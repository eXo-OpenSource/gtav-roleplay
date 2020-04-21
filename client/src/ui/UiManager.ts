import * as alt from 'alt';
import { FaceFeaturesUi } from './FaceFeaturesUi';
import { RegisterLogin } from './RegisterLogin';

export class UiManager {
    public static loadEvents() {
        alt.log('Loaded: UI Manager Events');
        
        
        alt.onServer('Ui:ShowFaceFeatures', () => new FaceFeaturesUi());
        
        alt.onServer('Ui:ShowRegisterLogin', () => new RegisterLogin());
    }   
}
