import React from 'react';

import dtf from 'dateformat';

import Main from '../components/Main.jsx';
import DateBox from '../components/DateBox.jsx';

import TransactionContainer from '../containers/TransactionContainer.jsx';

import ut from '../utils/Util.jsx';
import TransactionUtil from '../utils/TransactionUtil.jsx';

const MainContainer = React.createClass({
    dt (d) {
        return dtf(d, 'yyyy-mm-dd');
    },
    getInitialState () {

        //DEFAULT DATES FROM THREE DAYS BEFORE TODAY
        //AND ONE MONTHER AFTER TODAY
        var StartDate = new Date(this.dt(new Date()));
        var EndDate = new Date(this.dt(new Date()));

        //StartDate.setDate(StartDate.getDate() - 3);
        //EndDate.setMonth(EndDate.getMonth() + 1);

        StartDate.setDate(StartDate.getDate() - 3);
        EndDate.setDate(EndDate.getDate() + 3);

        return {
            StartDate,
            EndDate,
            Transactions: []
        }
    },
    handleChange (e) {
        this.updateTransactions();
    },
    handleDateSelect (dt) {
        //UPDATE START / END DATE FROM CHILD (DateBox) COMPONENT
        var st = this.state;
        st[dt.name] = dt.date;
        this.setState(st, this.updateTransactions);
    },
    updateTransactions() {
        TransactionUtil.get(
                this.dt(this.state.StartDate),
                this.dt(this.state.EndDate)
            )
            .then((transactions) => {
                transactions.data.map((v, i) => {
                    v.inEdit = false;
                    v.Amount = parseFloat(v.Amount).toFixed(2);
                    v.Balance = parseFloat(v.Balance).toFixed(2);
                });
                this.setState({
                    'Transactions': transactions.data
                });
            });
    },
    componentDidMount () {
        this.updateTransactions();
    },
    render () {
        if (this.state && this.state.Transactions) {
            return (
                <div>
                    <div className="container">
                        <div className="text-right col-sm-offset-6 col-sm-3">
                            <DateBox DateBoxName="StartDate"
                                     SetDate={this.state.StartDate}
                                     onDateSelect={this.handleDateSelect}
                                     Title="Start" />

                        </div>
                        <div className="text-right col-sm-3">
                            <DateBox DateBoxName="EndDate"
                                     SetDate={this.state.EndDate}
                                     onDateSelect={this.handleDateSelect}
                                     Title="End" />
                        </div>
                    </div>
                    <div className="container" >
                        <TransactionContainer Transactions={this.state.Transactions}
                                                handleChange={this.handleChange} />
                    </div>
                </div>
            )
        }
        return null;
    }
});

export default MainContainer;