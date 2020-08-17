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
		this.onRegisterClick = this.onRegisterClick.bind(this)

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
		if ('alt' in window) {
			alt.emit(
				'login',
				this.state.user,
				this.state.password
			);
		}
	}

	onRegisterClick() {
		this.setState({ error: "Derzeit können nur Betatester spielen." })
	}

	render() {
		return (
			<div>
				<div className="container mx-auto max-w-md" style={{ margin: "0 auto", marginTop: "12%" }}>
					<div className="card-header">Willkommen auf eXo:V!</div>
					<div className="card-body">
						<img className="mx-auto" src="https://forum.exo-reallife.de/wsc/images/styleLogo-09a90a2e08be66fccb657b42536567cf7dc373d9.png"></img>
						<p className="mt-4 text-white-alpha-80">Willkommen auf eXo:V!<br></br>Fülle das unten stehende Formular zum Anmelden aus.<br></br></p>
						<div className="mt-6 mb-6">
							<label htmlFor="user" className="block mb-2 text-gray-200">Spielername:</label>
							<input className="edit" value={this.state.user} id="user" name='user' onChange={this.onChange} type="text" placeholder="Forumname"/>
						</div>
						<label htmlFor="password" className="block mb-2 text-gray-200">Passwort:</label>
						<input className="edit" value={this.state.password} id="password" name="password" onChange={this.onChange} type="password" placeholder="***********"/>
						{this.state.error ? <div className="mb-2 mt-4 rounded bg-red-100 border-l-4 text-red-700 border-red-500 p-2">{this.state.error}</div> : null}
					</div>
					<div className="card-footer flex mx-auto">
						<button className="btn btn-primary" onClick={this.onLoginClick}>Server betreten</button>
						<button className="ml-12 btn text-white opacity-75 hover:opacity-100" onClick={this.onRegisterClick}>Auf Insel einschreiben</button>
					</div>
				</div>
			</div>
		);
	};
}

export default LoginForm;
