import React from 'react';
import { connect } from 'react-redux';

class MonthSelection extends React.Component<any, any> {
    componentDidMount() {
    }

    render() {
        const { user } = this.props;
        return (
            <div>
                <h1>Month Selection: {user.token}</h1>
            </div>
        );
    }
}


function mapStateToProps(state) {
    const { authentication } = state;
    const { user } = authentication;
    return {
        user
    };
}

const connectedMonthSelection = connect(mapStateToProps)(MonthSelection);
export { connectedMonthSelection as MonthSelection };
