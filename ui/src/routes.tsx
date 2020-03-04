import React from 'react';
import { Route, Router } from 'react-router';
import { LoginForm } from "./forms/login";

const createRoutes = () => (
    <Router>
        <Route exact path="/login" component={LoginForm} />
    </Router>
);

export default createRoutes;