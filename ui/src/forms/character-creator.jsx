import React, { Component } from 'react';

class CharacterCreatorForm extends Component {
    
    constructor(props) {
        super(props);
    }
    
    componentDidMount() {
        
    }
    
    render() {
        return <div className="container ml-auto mr-4 mt-12 max-w-md">
            <div className="card">
                <div className="card-header flex">
                    <div className="w-full">Aussehen</div>
                    <div className="w-full bg-gray-700 h-full">Gesicht</div>
                </div>
                <div className="card-body">
                    <div className="flex">
                        <div className="w-2/4 bg-indigo-400 py-8 rounded-md rounded-r-none">
                            <input className="hidden" type="radio" name="gender" value="m" checked/>
                            <p className="text-center text-white">Männlich</p>
                        </div>
                        <div className="w-2/4 bg-gray-700 py-8 rounded rounded-l-none">
                            <input className="hidden" type="radio" name="gender" value="w"/>
                            <p className="text-center text-white">Weiblich</p>
                        </div>
                    </div>
                </div>
                <div className="card-footer">
                    
                </div>
            </div>
        </div>;
    }
}

export default CharacterCreatorForm;