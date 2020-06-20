import React, { Component, CSSProperties } from 'react';
import { connect } from 'react-redux';
import { Dispatch, AnyAction, bindActionCreators } from 'redux';
// import { withRouter } from 'react-router-dom';

import { Row, Col, Button } from 'antd';

import { userLogout } from '../../user/actions';
import { Link } from 'react-router-dom';

const textWhite: CSSProperties = {
    color: "#ffffff"
}

class TopBar extends Component<ReturnType<typeof mapStateToProps> & ReturnType<typeof mapDispatchToProps>, {}> {
    componentDidMount() {
    }

    render() {
        const { user } = this.props;
        return (
            <Row>
                <Col offset={4} span={16}><h1 style={textWhite}>Harcama Takibi</h1></Col>
                <Col span={4} style={{textAlign: "right"}}>
                    <Link to='/logout'>
                        <Button type="dashed">Çıkış</Button>
                    </Link>
                </Col>
            </Row>
        );
    }
}


function mapStateToProps(state) {
    const { user } = state;
    return {
        user
    };
}

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>) =>
    bindActionCreators(
        {
            logout: userLogout
        },
        dispatch);

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(TopBar);
