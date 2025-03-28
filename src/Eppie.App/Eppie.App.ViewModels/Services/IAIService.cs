﻿// ---------------------------------------------------------------------------- //
//                                                                              //
//   Copyright 2025 Eppie (https://eppie.io)                                    //
//                                                                              //
//   Licensed under the Apache License, Version 2.0 (the "License"),            //
//   you may not use this file except in compliance with the License.           //
//   You may obtain a copy of the License at                                    //
//                                                                              //
//       http://www.apache.org/licenses/LICENSE-2.0                             //
//                                                                              //
//   Unless required by applicable law or agreed to in writing, software        //
//   distributed under the License is distributed on an "AS IS" BASIS,          //
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.   //
//   See the License for the specific language governing permissions and        //
//   limitations under the License.                                             //
//                                                                              //
// ---------------------------------------------------------------------------- //

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tuvi.Core.Entities;

namespace Eppie.App.ViewModels.Services
{
    /// <summary>
    /// Interface for AI service, providing methods for text processing, checking status, and managing the model.
    /// </summary>
    public interface IAIService
    {
        /// <summary>
        /// Event triggered when a local AI agent is added.
        /// </summary>
        event EventHandler<LocalAIAgentEventArgs> AgentAdded;
        /// <summary>
        /// Event triggered when a local AI agent is deleted.
        /// </summary>
        event EventHandler<LocalAIAgentEventArgs> AgentDeleted;
        /// <summary>
        /// Event triggered when a local AI agent is updated.
        /// </summary>
        event EventHandler<LocalAIAgentEventArgs> AgentUpdated;
        /// <summary>
        /// Event triggered when an exception occurs in the AI service.
        /// </summary>
        event EventHandler<ExceptionEventArgs> ExceptionOccurred;

        /// <summary>
        /// Asynchronously processes text with the specified local AI agent and language.
        /// </summary>
        /// <param name="agent">The local AI agent to be used for processing.</param>
        /// <param name="text">The text to be processed.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <param name="onTextUpdate">An optional action to be called with partial results as they are received.</param>
        /// <returns>A task representing the asynchronous operation, returning the processed text.</returns>
        Task<string> ProcessTextAsync(LocalAIAgent agent, string text, CancellationToken cancellationToken, Action<string> onTextUpdate = null);

        /// <summary>
        /// Returns whether the local AI is available.
        /// </summary>
        bool IsAvailable();

        /// <summary>
        /// Asynchronously checks if the local AI is enabled.
        /// </summary>        
        Task<bool> IsEnabledAsync();

        /// <summary>
        /// Asynchronously checks if the local AI model is imported.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, returning true if the model is imported, otherwise false.</returns>
        Task<bool> IsLocalAIModelImportedAsync();

        /// <summary>
        /// Asynchronously deletes the local AI model.
        /// </summary>
        Task DeleteModelAsync();

        /// <summary>
        /// Asynchronously imports the local AI model.
        /// </summary>
        Task ImportModelAsync();

        /// <summary>
        /// Adds a local AI agent to the service.
        /// </summary>
        /// <param name="agent">The local AI agent to be added.</param>
        Task AddAgentAsync(LocalAIAgent agent);

        /// <summary>
        /// Removes a local AI agent from the service.
        /// </summary>
        /// <param name="agent">The local AI agent to be removed.</param>
        Task RemoveAgentAsync(LocalAIAgent agent);

        /// <summary>
        /// Gets a read-only collection of local AI agents.
        /// </summary>
        /// <returns>A read-only collection of local AI agents.</returns>
        Task<IReadOnlyList<LocalAIAgent>> GetAgentsAsync();

        /// <summary>
        /// Updates a local AI agent in the service.
        /// </summary>
        /// <param name="agent">The local AI agent to be updated.</param>
        Task UpdateAgentAsync(LocalAIAgent agent);
    }
}
