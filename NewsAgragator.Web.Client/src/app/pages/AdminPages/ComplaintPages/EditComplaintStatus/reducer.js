import initialState from "../../../../../initialState";



export default (state = initialState.editComplaintStatus, action) => {
    switch (action.type) {

        case "COMPLAINTSTATUS_SELECT":
            return {
                ...state,
                // editComplaintStatus:
                // {
                //     ...state.editComplaintStatus,
                    [action.name]:
                    {
                        value: action.val,
                    },
                // }
            }

        case "COMPLAINTSTATUS_LOAD_START":
            return {
                ...state,
                // editComplaintStatus:
                // {
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
                // }
            }

        case "COMPLAINTSTATUS_LOAD_SUCCESS":
            return {
                ...state,
                // editComplaintStatus:
                // {
                //     ...state.editComplaintStatus,
                    loading: false,
                    ...Object.fromEntries(Object.entries(action.data).map(x => ([
                        x[0],
                        {
                            value: x[1],
                        }
                    ]))),
                // }
            }

        default:
            return state
    }
}