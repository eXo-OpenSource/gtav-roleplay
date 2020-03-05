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
        return <div className="container mx-auto rounded shadow-lg max-w-md">
                   <div className="" style={{ margin: "0 auto", marginTop: "20%" }}>
                       <div className="px-6">
                           <div className="px-4 py-4 text-xl">Login</div>
                           <div className="card-body">
                               {this.state.error ? <div className="alert alert-danger">{this.state.error}</div> : null}
                               <p>Benutzername:</p>
                               <input className="border rounded w-full appearance-none py-2" value={this.state.name} name='user' onChange={this.onChange} type="text"/>
                               <p>Passwort:</p>
                               <input className="form-control" value={this.state.password} name="password" onChange={this.onChange} type="password"/>
                           </div>
                           <div className="card-footer">
                               <button className="btn btn-primary" onClick={this.onLoginClick}>Login</button>
                           </div>
                       </div>
                   </div>
               </div>;
    };
}

export default LoginForm;
