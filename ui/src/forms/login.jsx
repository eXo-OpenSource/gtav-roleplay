import React, { Component } from 'react';

class LoginForm extends Component {

    constructor(props) {
        super(props)
        this.state = {
            user: "",
            password: "",
            error: false
        }

        this.onChange = this.onChange.bind(this)
        this.onLoginClick = this.onLoginClick.bind(this)

    }

    componentDidMount() {
        if ('alt' in window) {
            alt.on('setError', this.setError.bind(this));
        }
    }

    setError(error) {
        this.setState({ error: error })
    }

    onChange(e) {
        var currentState = this.state;
        currentState[e.target.name] = e.target.value;
        this.setState({ data: currentState });
    }

    onLoginClick() {
        console.log(this.state.user)
        if ('alt' in window) {
            alt.emit(
                'login',
                this.state.user,
                this.state.password
            );
        }
    }

    render() {
        return (
            <div className="container mx-auto max-w-md" style={{ margin: "0 auto", marginTop: "10%" }}>
                <div className="card">
                    <div className="card-header">Login</div>
                    <div className="card-body">
                        {this.state.error ? <div className="mb-2 rounded bg-red-100 border-l-4 text-red-700 border-red-500 p-2">{this.state.error}</div> : null}
                        <div className="mb-6">
                            <label htmlFor="user" className="block mb-2">Benutzername:</label>
                            <input className="bg-gray-400 rounded w-full appearance-none py-2 px-3" value={this.state.user} id="user" name='user' onChange={this.onChange} type="text"/>
                        </div>
                        <label htmlFor="password" className="block mb-2">Passwort:</label>
                        <input className="bg-gray-400 rounded w-full appearance-none py-2 px-3" value={this.state.password} id="password" name="password" onChange={this.onChange} type="password"/>
                    </div>
                    <div className="card-footer">
                        <button className="btn btn-primary" onClick={this.onLoginClick}>Login</button>
                    </div>
                </div>
            </div>
        );
    };
}

export default LoginForm;
