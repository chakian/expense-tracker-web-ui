import React, { Component } from 'react';

class PublicLayout extends Component {
    render() {
        return (
            <div>
                <h2>PUBLIC</h2>
                {this.props.children}
            </div>
        );
    }
}

export { PublicLayout };