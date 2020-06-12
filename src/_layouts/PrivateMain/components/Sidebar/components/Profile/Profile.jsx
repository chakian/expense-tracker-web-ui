import React from 'react';
import { Link as RouterLink } from 'react-router-dom';
import clsx from 'clsx';
import { makeStyles } from '@material-ui/styles';
import { Avatar, Typography } from '@material-ui/core';
import { connect } from 'react-redux';

class Profile extends React.Component {
    componentDidMount() {
    }

    render() {
        const { className, user, ...rest } = this.props;
        const classes = makeStyles(theme => ({
            root: {
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
                minHeight: 'fit-content'
            },
            avatar: {
                width: 60,
                height: 60
            },
            name: {
                marginTop: theme.spacing(1)
            }
        }));

        const prfl = {
            name: user.name,
            avatar: '/images/avatars/avatar_11.png',
            bio: 'Brain Director'
        };

        return (
            <div
                {...rest}
                className={clsx(classes.root, className)}
            >
                <Avatar
                    alt="Person"
                    className={classes.avatar}
                    component={RouterLink}
                    src={prfl.avatar}
                    to="/settings"
                />
                <Typography
                    className={classes.name}
                    variant="h4"
                >
                    {prfl.name}
                </Typography>
                <Typography variant="body2">{prfl.bio}</Typography>
            </div>
        );
    }
}

function mapStateToProps(state) {
    const { className, authentication } = state;
    const { user } = authentication;
    return {
        className, user
    };
}

const connectedProfile = connect(mapStateToProps)(Profile);
export { connectedProfile as Profile };
