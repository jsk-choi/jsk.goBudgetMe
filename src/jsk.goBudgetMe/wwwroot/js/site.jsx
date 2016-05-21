/// <reference path="~/_references.js" />

var GbmApp = React.createClass({
    loadTranFromServer: function () {
        console.log(this.props.url);
        $.ajax({
            url: this.props.url,
            dataType: 'json',
            cache: false,
            success: function (data) {
                console.log(data);
                this.setState({ data: data });
                console.log('state: ' + JSON.stringify(this.state));
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(this.props.url, status, err.toString());
            }.bind(this)
        });
    },
    handleTransactionSubmit: function (tran) {
    },
    getInitialState: function () {
        return { data: [] };
    },
    componentWillMount: function () {
        console.log('will mount');
    },
    componentDidMount: function () {
        console.log('did mount');
        this.loadTranFromServer();
    },
    render: function () {
        return (
            <h1>nopie</h1>
        );
    }
});

ReactDOM.render(
    <GbmApp url="/api/values" pollInterval={5000} />,
    document.getElementById('content')
);










