import React from 'react';
import { connect } from 'react-redux';

class MonthSelection extends React.Component {
    componentDidMount() {
    }

    render() {
        return (
            <div>
                <h1>Month Selection</h1>
            </div>
        );
    }
}


function mapStateToProps(state) {
    return {
    };
}

const connectedMonthSelection = connect(mapStateToProps)(MonthSelection);
export { connectedMonthSelection as MonthSelection };
