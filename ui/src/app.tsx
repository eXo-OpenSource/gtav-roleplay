declare var require: any

var React = require('react');
var ReactDOM = require('react-dom');

export class App extends React.Component {
    render() {
        return (
            <h1>Welcome to React!!</h1>
        );
    }
}

ReactDOM.render(<App />, document.getElementById('root'));
