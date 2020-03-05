import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import { HashRouter, Route } from "react-router-dom";
import LoginForm from "./forms/login";

import './root.css';

class App extends Component {
    render() {
        return (
            <HashRouter>
                <Route path="/login" component={LoginForm}/>
            </HashRouter>
    );
    }
}

ReactDOM.render(<App />, document.getElementById('root'));
