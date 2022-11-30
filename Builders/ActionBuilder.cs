using System;
using System.Threading.Tasks;
using MoBro.Plugin.SDK.Enums;
using MoBro.Plugin.SDK.Models;
using MoBro.Plugin.SDK.Models.Actions;
using Action = System.Action;

namespace MoBro.Plugin.SDK.Builders;

/// <summary>
/// Builder to create a new <see cref="IAction"/>
/// </summary>
public sealed class ActionBuilder :
  ActionBuilder.IIdStage,
  ActionBuilder.ILabelStage,
  ActionBuilder.ICategoryStage,
  ActionBuilder.IGroupStage,
  ActionBuilder.IHandlerStage,
  ActionBuilder.IBuildStage
{
  private string? _id;
  private string? _label;
  private string? _description;
  private string? _categoryId;
  private string? _groupId;
  private Func<IMoBroSettings, Task<object?>>? _handler;
  private bool _returnsResult;

  private ActionBuilder()
  {
  }

  internal static IIdStage CreateAction()
  {
    return new ActionBuilder();
  }

  /// <inheritdoc />
  public ILabelStage WithId(string id)
  {
    _id = id;
    return this;
  }

  /// <inheritdoc />
  public ICategoryStage WithLabel(string label, string? description = null)
  {
    _label = label;
    _description = description;
    return this;
  }

  /// <inheritdoc />
  public IGroupStage OfCategory(string categoryId)
  {
    _categoryId = categoryId;
    return this;
  }

  /// <inheritdoc />
  public IGroupStage OfCategory(CoreCategory coreCategory)
  {
    _categoryId = coreCategory.ToString().ToLower();
    return this;
  }

  /// <inheritdoc />
  public IGroupStage OfCategory(ICategory category)
  {
    _categoryId = category.Id;
    return this;
  }

  /// <inheritdoc />
  public IGroupStage OfNoCategory()
  {
    _categoryId = CoreCategory.Miscellaneous.ToString().ToLower();
    return this;
  }

  /// <inheritdoc />
  public IHandlerStage OfGroup(string? groupId)
  {
    _groupId = groupId;
    return this;
  }

  /// <inheritdoc />
  public IHandlerStage OfGroup(IGroup group)
  {
    _groupId = group.Id;
    return this;
  }

  /// <inheritdoc />
  public IHandlerStage OfNoGroup()
  {
    _groupId = null;
    return this;
  }

  /// <inheritdoc />
  public IBuildStage WithHandler(Action<IMoBroSettings> handler)
  {
    _returnsResult = false;
    _handler = p =>
    {
      handler.Invoke(p);
      return Task.FromResult<object?>(null);
    };
    return this;
  }

  /// <inheritdoc />
  public IBuildStage WithHandler(Action handler)
  {
    _returnsResult = false;
    _handler = _ =>
    {
      handler.Invoke();
      return Task.FromResult<object?>(null);
    };
    return this;
  }

  /// <inheritdoc />
  public IBuildStage WithHandler(Func<IMoBroSettings, object?> handler)
  {
    _returnsResult = true;
    _handler = p => Task.FromResult(handler.Invoke(p));
    return this;
  }

  /// <inheritdoc />
  public IBuildStage WithHandler(Func<object?> handler)
  {
    _returnsResult = true;
    _handler = _ => Task.FromResult(handler.Invoke());
    return this;
  }

  /// <inheritdoc />
  public IBuildStage WithAsyncHandler(Func<Task> handler)
  {
    _returnsResult = false;
    _handler = async _ =>
    {
      await handler.Invoke();
      return Task.FromResult<object?>(null);
    };
    return this;
  }

  /// <inheritdoc />
  public IBuildStage WithAsyncHandler(Func<IMoBroSettings, Task> handler)
  {
    _returnsResult = false;
    _handler = async p =>
    {
      await handler.Invoke(p);
      return Task.FromResult<object?>(null);
    };
    return this;
  }

  /// <inheritdoc />
  public IBuildStage WithAsyncHandler(Func<Task<object?>> handler)
  {
    _returnsResult = true;
    _handler = _ => handler.Invoke();
    return this;
  }

  /// <inheritdoc />
  public IBuildStage WithAsyncHandler(Func<IMoBroSettings, Task<object?>> handler)
  {
    _returnsResult = true;
    _handler = handler;
    return this;
  }

  /// <inheritdoc />
  public IAction Build()
  {
    return new Models.Actions.Action(
      _id ?? throw new InvalidOperationException("Action id must not be null"),
      _label ?? throw new InvalidOperationException("Action label must not be null"),
      _categoryId ?? CoreCategory.Miscellaneous.ToString().ToLower(),
      _handler ?? (_ => Task.FromResult<object?>(null))
    )
    {
      Description = _description,
      GroupId = _groupId,
      ReturnsResult = _returnsResult
    };
  }

  /// <summary>
  /// Building stage of the <see cref="ActionBuilder"/>
  /// </summary>
  public interface IIdStage
  {
    /// <summary>
    /// Sets the id of the <see cref="IAction"/>
    /// </summary>
    /// <param name="id">The id (must be unique within the scope of the plugin)</param>
    /// <returns>The next building stage</returns>
    public ILabelStage WithId(string id);
  }

  /// <summary>
  /// Building stage of the <see cref="ActionBuilder"/>
  /// </summary>
  public interface ILabelStage
  {
    /// <summary>
    /// Sets the label and optionally a description of the <see cref="IAction"/>
    /// </summary>
    /// <param name="label">The label</param>
    /// <param name="description">The optional textual description</param>
    /// <returns>The next building stage</returns>
    public ICategoryStage WithLabel(string label, string? description = null);
  }

  /// <summary>
  /// Building stage of the <see cref="ActionBuilder"/>
  /// </summary>
  public interface ICategoryStage
  {
    /// <summary>
    /// Sets the <see cref="ICategory"/> this <see cref="IAction"/> belongs to
    /// </summary>
    /// <param name="categoryId">The id of the <see cref="ICategory"/></param>
    /// <returns>The next building stage</returns>
    public IGroupStage OfCategory(string categoryId);

    /// <summary>
    /// Sets the <see cref="ICategory"/> this <see cref="IAction"/> belongs to
    /// </summary>
    /// <param name="coreCategory">The <see cref="CoreCategory"/></param>
    /// <returns>The next building stage</returns>
    public IGroupStage OfCategory(CoreCategory coreCategory);

    /// <summary>
    /// Sets the <see cref="ICategory"/> this <see cref="IAction"/> belongs to
    /// </summary>
    /// <param name="category">The <see cref="ICategory"/></param>
    /// <returns>The next building stage</returns>
    public IGroupStage OfCategory(ICategory category);

    /// <summary>
    /// Sets the <see cref="ICategory"/> of this <see cref="IAction"/> to the default value of
    /// <see cref="CoreCategory.Miscellaneous"/>
    /// </summary>
    /// <returns>The next building stage</returns>
    public IGroupStage OfNoCategory();
  }

  /// <summary>
  /// Building stage of the <see cref="ActionBuilder"/>
  /// </summary>
  public interface IGroupStage
  {
    /// <summary>
    /// Sets the <see cref="IGroup"/> this <see cref="IAction"/> belongs to. A null value indicates 'no group' and
    /// is the same as calling <see cref="OfNoGroup()"/>
    /// </summary>
    /// <param name="groupId">The id of the <see cref="IGroup"/></param>
    /// <returns>The next building stage</returns>
    public IHandlerStage OfGroup(string? groupId);

    /// <summary>
    /// Sets the <see cref="IGroup"/>
    /// this <see cref="IAction"/> belongs to
    /// </summary>
    /// <param name="group">The <see cref="IGroup"/></param>
    /// <returns>The next building stage</returns>
    public IHandlerStage OfGroup(IGroup group);

    /// <summary>
    /// Builds the  <see cref="IAction"/> without 
    /// a <see cref="IGroup"/>
    /// </summary>
    /// <returns>The next building stage</returns>
    public IHandlerStage OfNoGroup();
  }

  /// <summary>
  /// Building stage of the <see cref="ActionBuilder"/>
  /// </summary>
  public interface IHandlerStage
  {
    /// <summary>
    /// Sets the handler that will be called whenever the action is invoked
    /// </summary>
    /// <param name="handler">The handler</param>
    /// <returns>The next building stage</returns>
    public IBuildStage WithHandler(Action<IMoBroSettings> handler);

    /// <summary>
    /// Sets the handler that will be called whenever the action is invoked
    /// </summary>
    /// <param name="handler">The handler</param>
    /// <returns>The next building stage</returns>
    public IBuildStage WithHandler(System.Action handler);

    /// <summary>
    /// Sets the handler that will be called whenever the action is invoked
    /// </summary>
    /// <param name="handler">The handler</param>
    /// <returns>The next building stage</returns>
    public IBuildStage WithHandler(Func<IMoBroSettings, object?> handler);

    /// <summary>
    /// Sets the handler that will be called whenever the action is invoked
    /// </summary>
    /// <param name="handler">The handler</param>
    /// <returns>The next building stage</returns>
    public IBuildStage WithHandler(Func<object?> handler);

    /// <summary>
    /// Sets the handler that will be called whenever the action is invoked
    /// </summary>
    /// <param name="handler">The handler</param>
    /// <returns>The next building stage</returns>
    public IBuildStage WithAsyncHandler(Func<Task> handler);

    /// <summary>
    /// Sets the handler that will be called whenever the action is invoked
    /// </summary>
    /// <param name="handler">The handler</param>
    /// <returns>The next building stage</returns>
    public IBuildStage WithAsyncHandler(Func<IMoBroSettings, Task> handler);

    /// <summary>
    /// Sets the handler that will be called whenever the action is invoked
    /// </summary>
    /// <param name="handler">The handler</param>
    /// <returns>The next building stage</returns>
    public IBuildStage WithAsyncHandler(Func<Task<object?>> handler);

    /// <summary>
    /// Sets the handler that will be called whenever the action is invoked
    /// </summary>
    /// <param name="handler">The handler</param>
    /// <returns>The next building stage</returns>
    public IBuildStage WithAsyncHandler(Func<IMoBroSettings, Task<object?>> handler);
  }

  /// <summary>
  /// Building stage of the <see cref="ActionBuilder"/>
  /// </summary>
  public interface IBuildStage
  {
    /// <summary>
    /// Builds and creates the <see cref="IAction"/>.
    /// </summary>
    /// <returns>The <see cref="IAction"/></returns>
    public IAction Build();
  }
}