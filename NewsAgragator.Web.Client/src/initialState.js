import { text } from "@fortawesome/fontawesome-svg-core";
import { clientMain, clientNews, comments, createComplaint } from "./reducers";

export default {

    editAccount: {
        loadingParameters: true,
        loadingData:false,
        id: {
            value: '',
        },
        userName: {
            value: '',
        },
        fio: {
            value: '',
        },
        email: {
            value: '',
        },
        phone: {
            value: '',
        },
        desiredNewsRating: {
            value: '',
        },
        login: {
            value: '',
        },
        password: {
            value: '',
        },
        accountStatuses: {
            value: "",
            text: "",
            options: [
                {
                    id: '',
                    text: ''
                },
            ],
        },
        roles: {
            value: "",
            text: "",
            options: [
                {
                    id: '',
                    text: ''
                },
            ],
        },
    },

    accounts: {
        loading: true,
        value:
            [
                {
                    id: {
                        value: '',
                    },
                    userName: {
                        value: '',
                    },
                    fIO: {
                        value: '',
                    },
                    email: {
                        value: '',
                    },
                    phone: {
                        value: '',
                    },
                    desiredRating: {
                        value: '',
                    },
                    login: {
                        value: '',
                    },
                    password: {
                        value: '',
                    },
                    accountStatus: {
                        value: '',
                    },
                    role: {
                        value: '',
                    },
                },
            ],
    },

    editComplaint: {
        loading: true,
        id: {
            value: '',
        },
        title: {
            value: '',
        },
        text: {
            value: '',
        },

        comment: {
            value: '',
            options: {
                text: '',
                value: '',
            }
        },

        news: {
            value: '',
            options: {
                text: '',
                value: '',
            }
        },

        complaintStatus: {
            value: '',
            options: {
                text: '',
                value: '',
            }
        },

        complaintType: {
            value: '',
            options: {
                text: '',
                value: '',
            }
        },

        user: {
            value: '',
            options: {
                text: '',
                value: '',
            }
        },

        administrator: {
            value: '',
            options: {
                text: '',
                value: '',
            }
        },
    },

    complaints: {
        loading: true,
        value:
            [
                {
                    id: {
                        value: '',
                    },
                    title: {
                        value: '',
                    },
                    text: {
                        value: '',
                    },

                    comment: {
                        value: '',
                    },

                    news: {
                        value: '',
                    },

                    complaintStatus: {
                        value: '',
                    },

                    complaintType: {
                        value: '',
                    },

                    user: {
                        value: '',
                    },

                    administrator: {
                        value: '',
                    },
                },
            ],
    },

    editComment: {
        loading: true,
        id: {
            value: '',
        },
        text: {
            value: '',
        },
        date: {
            value: '',
        },

        account: {
            value: '',
            options: {
                text: '',
                value: '',
            }
        },

        news: {
            value: '',
            options: {
                text: '',
                value: '',
            }
        },
    },

    comments: {
        loading: true,
        value:
            [
                {
                    id: {
                        value: '',
                    },
                    text: {
                        value: '',
                    },
                    date: {
                        value: '',
                    },

                    account: {
                        value: '',
                    },

                    news: {
                        value: '',
                    },
                },
            ],
    },

    editNews: {
        loading: true,
        id: {
            value: '',
        },
        title: {
            value: '',
        },
        text: {
            value: '',
        },
        textHTML: {
            value: '',
        },
        description: {
            value: '',
        },
        sourceUrl: {
            value: '',
        },
        date: {
            value: '',
        },
        positiveRating: {
            value: '',
        },

        newsStatus: {
            value: '',
            options: {
                text: '',
                value: '',
            }
        },
        newsStatuses: [{
            value: '',
            text: '',
        }]
    },

    news: {
        loading: true,
        value:
            [
                {
                    id: {
                        value: '',
                    },
                    title: {
                        value: '',
                    },
                    text: {
                        value: '',
                    },
                    date: {
                        value: '',
                    },
                    positiveRating: {
                        value: '',
                    },

                    newsStatus: {
                        value: '',
                    },
                },
            ],
    },

    editNotificationMessage: {
        loading: true,
        id: {
            value: '',
        },
        title: {
            value: '',
        },
        text: {
            value: '',
        },

        user: {
            value: '',
            options: {
                text: '',
                value: '',
            }
        },
        administrator: {
            value: '',
            options: {
                text: '',
                value: '',
            }
        },
    },

    notificationMessages: {
        loading: true,
        value:
            [
                {
                    id: {
                        value: '',
                    },
                    title: {
                        value: '',
                    },
                    text: {
                        value: '',
                    },

                    user: {
                        value: '',
                    },
                    administrator: {
                        value: '',
                    },
                },
            ],
    },

    editWeather: {
        loading: true,
        id: {
            value: '',
        },
        city: {
            value: '',
        },
        temperatureMorning: {
            value: '',
        },
        temperatureDay: {
            value: '',
        },
        temperatureEvening: {
            value: '',
        },
        temperatureNight: {
            value: '',
        },
        temperatureCommon: {
            value: '',
        },
        date: {
            value: '',
        },
        percipitaion: {
            value: '',
        },
        wind: {
            value: '',
        },
        windDirection: {
            value: '',
        },
        pressure: {
            value: '',
        },
        humidity: {
            value: '',
        },

        weatherStatusMorning: {
            value: '',

        },

        weatherStatusDay: {
            value: '',

        },

        weatherStatusEvening: {
            value: '',

        },

        weatherStatusNight: {
            value: '',
            options: {
                text: '',
                value: '',
            }
        },

        weatherStatusCommon: {
            value: '',
            options: {
                text: '',
                value: '',
            }
        },
    },

    weathers: {
        loading: true,
        value:
        [
            {
                id: {
                    value: '',
                },
                city: {
                    value: '',
                },
                temperatureMorning: {
                    value: '',
                },
                temperatureDay: {
                    value: '',
                },
                temperatureEvening: {
                    value: '',
                },
                temperatureNight: {
                    value: '',
                },
                temperatureCommon: {
                    value: '',
                },
                date: {
                    value: '',
                },
                percipittaion: {
                    value: '',
                },
                wind: {
                    value: '',
                },
                windDirection: {
                    value: '',
                },
                pressure: {
                    value: '',
                },
                humidity: {
                    value: '',
                },

                weatherStatusMorning: {
                    value: '',
                },

                weatherStatusDay: {
                    value: '',
                },

                weatherStatusEvening: {
                    value: '',
                },

                weatherStatusNight: {
                    value: '',
                },

                weatherStatusCommon: {
                    value: '',
                },
            },
        ],
    },

    editAccountStatus: {
        loading: true,
        id: {
            value: '',
        },
        title: {
            value: '123',
        },
        description: {
            value: 'qwe',
        }
    },

    accountStatuses: {
        loading: true,
        value:
            [
                {
                    id: {
                        value: '',
                    },
                    title: {
                        value: '',
                    },
                    description: {
                        value: '',
                    },
                },
            ],
    },

    editNewsStatus: {
        loading: true,
        id: {
            value: '',
        },
        title: {
            value: '',
        },
        description: {
            value: '',
        }
    },

    newsStatuses: {
        loading: true,
        value:
            [
                {
                    id: {
                        value: '',
                    },
                    title: {
                        value: '',
                    },
                    description: {
                        value: '',
                    },
                },
            ],
    },

    editWeatherStatus: {
        loading: true,
        id: {
            value: '',
        },
        title: {
            value: '',
        },
        description: {
            value: '',
        }
    },

    weatherStatuses: {
        loading: true,
        value:
            [
                {
                    id: {
                        value: '',
                    },
                    title: {
                        value: '',
                    },
                    description: {
                        value: '',
                    },
                },
            ],
    },

    editPolicy: {
        loading: true,
        id: {
            value: '',
        },
        title: {
            value: '',
        },
        description: {
            value: '',
        }
    },

    policies: {
        loading: true,
        value:
            [
                {
                    id: {
                        value: '',
                    },
                    title: {
                        value: '',
                    },
                    description: {
                        value: '',
                    },
                },
            ],
    },

    editComplaintStatus: {
        loading: true,
        id: {
            value: '',
        },
        title: {
            value: '',
        },
        description: {
            value: '',
        }
    },

    complaintStatuses: {
        loading: true,
        value:
            [
                {
                    id: {
                        value: '',
                    },
                    title: {
                        value: '',
                    },
                    description: {
                        value: '',
                    },
                },
            ],
    },

    editComplaintType: {
        loading: true,
        id: {
            value: '',
        },
        title: {
            value: '',
        },
        description: {
            value: '',
        }
    },

    complaintTypes: {
        loading: true,
        value:
            [
                {
                    id: {
                        value: '',
                    },
                    title: {
                        value: '',
                    },
                    description: {
                        value: '',
                    },
                },
            ],
    },

    editRole: {
        id: {
            value: '',
        },
        title: {
            value: '',
        },
        description: {
            value: '',
        },
        policies:{
            value:[],
            options:[],
        },
    },

    roles: {
        loading: true,
        value:
        [
            {
                id: {
                    value: '',
                },
                title: {
                    value: '',
                },
                description: {
                    value: '',
                },
                policies:[{
                    id:'',
                    text:'',
                }]
            },
        ],
    },

    account: {

    },

    weather: {

    },

    news: {

    },
    complaint: {

    },

    comment: {

    },

    notificationMessage: {

    },

    clientMain: {
        loading: true,
        topNewses:
        [
            {
                id: {
                    value: '',
                },
                title: {
                    value: '',
                },
                text: {
                    value: '',
                },
                date: {
                    value: '',
                },
                positiveRating: {
                    value: '',
                },
                description: {
                    value:'',
                },
            },
        ],
    },

    clientNews:{
        loading: true,
        value:
        [
            {
                id: {
                    value: '',
                },
                title: {
                    value: '',
                },
                text: {
                    value: '',
                },
                date: {
                    value: '',
                },
                positiveRating: {
                    value: '',
                },
                description: {
                    value:'',
                }, 
            },
        ],
    },

    createComplaint: {
        loading: true,
        id: {
            value: '',
        },
        title: {
            value: '',
        },
        text: {
            value: '',
        },
        complaintStatusId: {
            value: '',
        },
        complaintTypeId: {
            value: '',
        },
        userId: {
            value: '',
        },
     
    },

    clientNewsFull: {
        loadingParameters: true,
        loadingData:true,
        id: {
            value: '',
        },
        title: {
            value: '',
        },
        text: {
            value: '',
        },
        textHTML: {
            value: '',
        },
        description: {
            value: '',
        },
        sourceUrl: {
            value: '',
        },
        date: {
            value: '',
        },
        positiveRating: {
            value: '',
        },
        comments:[],
    },

    signUP: {
        isSuccess: false,
        loading: false,
        error: "",

        login: {
            value: '',
        },
        password: {
            value: '',
        },
        confirmationPassword: {
            value: '',
        },
        userName: {
            value: '',
        },
        fio: {
            value: '',
        },
        email: {
            value: '',
        },
        phone: {
            value: '',
        },
        desiredNewsRating: {
            value: '',
        },
        secretWord: {
            value: '',
        },
    },

    signIN: {
        isSuccess: false,
        loading: false,
        error: "",
        login: "",
        password: "",
        
    },
    authorization: {
        atoken: null,
        rtoken:null,
        userName: null,
        role:null,
        ...JSON.parse(localStorage.getItem("AUTHORIZATION"))
    },

}