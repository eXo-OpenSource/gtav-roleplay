import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter, Route } from "react-router-dom";
import LoginForm from "./forms/login";

import './root.css';

class App extends Component {
    render() {
        return (
            <BrowserRouter>
                <Route path="/login" component={LoginForm}/>
            </BrowserRouter>
    );
    }
}

ReactDOM.render(<App />, document.getElementById('root'));
