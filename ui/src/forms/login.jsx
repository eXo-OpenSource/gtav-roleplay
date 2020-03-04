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
        return <div className="container">
                   <div className="col-4" style={{ margin: "0 auto", marginTop: "20%" }}>
                       <div className="card">
                           <div className="card-header">Login</div>
                           <div className="card-body">
                               {this.state.error ? <div class="alert alert-danger">{this.state.error}</div> : null}
                               <p>Benutzername:</p>
                               <input className="form-control" value={this.state.name} name='user' onChange={this.onChange} type="text"></input>
                               <p>Passwort:</p>
                               <input className="form-control" value={this.state.password} name="password" onChange={this.onChange} type="password"></input>
                           </div>
                           <div className="card-footer">
                               <button class="btn btn-primary" onClick={this.onLoginClick}>Login</button>
                           </div>
                       </div>
                   </div>
               </div>;
    };
}

export default LoginForm;
